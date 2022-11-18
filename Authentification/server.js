//Requierements
const express = require("express");
const bodyParser = require("body-parser");
const cors = require("cors");
const app = express();
require('dotenv').config()

//Eureka
const Eureka = require('eureka-js-client').Eureka;

const client = new Eureka({
  // application instance information
  instance: {
    app: 'AUTH-SERVICE',
    instanceId: 'authServiceId',
    hostName: 'localhost',
    ipAddr: '127.0.0.1',
    port:  {
        '$': 8000,
        '@enabled': 'true',
    },
    vipAddress: 'AUTH-SERVICE',
    statusPageUrl: 'http://localhost:8000/info',
    dataCenterInfo:  {
        '@class': 'com.netflix.appinfo.InstanceInfo$DefaultDataCenterInfo',
        name: 'MyOwn',
    },
    registerWithEureka: true,
    fetchRegistry: true
  },
  eureka: {
      host: '10.111.21.78',
      serviceUrls: 'http://10.111.21.78:9000/',
      port: 9000,
      servicePath: '/eureka/apps/'
  },
});

client.logger.level('debug');
  
client.start((error) => {
  console.log(error || 'complete');
});

//RABBITMQ
const CONN_URL = 'amqps://ekocwjow:Todj_aar9SDbpLPP_tr0mL4-qTB4gGCQ@rat.rmq2.cloudamqp.com/ekocwjow';

const amqp = require("amqplib");
async function connect() {
 try {
   const connection = await amqp.connect("amqps://ekocwjow:Todj_aar9SDbpLPP_tr0mL4-qTB4gGCQ@rat.rmq2.cloudamqp.com/ekocwjow");
   const connectionDamien = await amqp.connect("amqp://guest:guest@10.111.21.78:5672");
   
   const channel = await connectionDamien.createChannel();
   await channel.assertQueue("Authentification");
   console.log("finit...");
   channel.consume("Authentification", message => {
     const input = JSON.parse(message.content.toString());
     console.log(`Received Authentification: ${input.number}`);
     channel.ack(message);
   });
   console.log(`Waiting for messages...`);
 } catch (ex) {
   console.error(ex);
 }
}
connect();

//Using App Express
var corsOptions = {
  origin: "http://localhost:8000"
};
app.use(cors(corsOptions));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.json());

//DATABASE
const db = require("./models");
db.sequelize.sync();
// db.sequelize.sync({ force: true }).then(() => {
//     console.log("Alter and re-sync db.");
//   });

//Route racine
app.get("/", (req, res) => {
  res.json({ message: "Express API is Ready" });
});

//Routes
require('./routes/auth.routes')(app);
require('./routes/user.routes')(app);

//Start Application
const PORT = process.env.PORT || 8080;
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}.`);
});