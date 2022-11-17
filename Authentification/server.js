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
      app: 'authService',
      instanceId: 'authServiceId',
      port: {
          '$': 8000,
          '@enabled': 'true',
      },
      vipAddress: 'authService',
      dataCenterInfo: {
          '@class': 'com.netflix.appinfo.InstanceInfo$DefaultDataCenterInfo',
          name: 'MyOwn',
      },
      registerWithEureka: true,
      fetchRegistry: true 
  },
  eureka: {
      host: '192.168.43.210',
      port: 9000
  },
   });
  
     client.logger.level('debug');
     client.start((error) => {
             console.log(error || 'complete');
      });

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