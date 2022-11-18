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
// const amqp = require("amqplib");

// var channel, connection;  //global variables
// async function connectQueue() {   
//     try {
//         connection = await amqp.connect("amqp://10.111.21.78:5672");
//         channel    = await connection.createChannel()
        
//         await channel.assertQueue("test-queue")
        
//     } catch (error) {
//         console.log(error)
//     }
// }
// connectQueue();

// async function sendData (data) {
//   // send data to queue
//   await channel.sendToQueue("Login-queue", Buffer.from(JSON.stringify(data)));
      
//   // close the channel and connection
//   await channel.close();
//   await connection.close(); 
// }

// app.get("/api/auth/signin", (req, res) => {
    
//   // data to be sent
//   const data = {
//       username  : req.body.username,
//       password : req.body.password
//   }
//   sendData(data);  // pass the data to the function we defined
//   console.log("A message is sent to queue")
//   res.send("Message Sent"); //response to the API request
  
// })

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