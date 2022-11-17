const jwt = require("jsonwebtoken");
const config = require("../config/auth.config.js");
const db = require("../models");
const User = db.users;

const verifyToken = (req, res, next) => {

  let token = req.headers["x-access-token"];

  if (!token) {

    return res.status(403).send({
      message: "Pas de token fourni !"
    });

  }

  jwt.verify(token, config.secret, (err, decoded) => {

    if (err) {

      return res.status(401).send({
        message: "Pas autorisé !"
      });

    }

    req.userId = decoded.id;
    next();

  });

};

const isAdmin = (req, res, next) => {

    User.findOne({ where: { ID: req.userId, roleAdmin: 1 }}).then(user => { 
      
      if (!user) {

        res.status(400).send({
            message: "Cet utilisateur n'est pas trouvé ou n'est pas administrateur !"
        });
        
        return;
      } else {

        res.status(200).send({
          message: "Utilisateur (administrateur) trouvé et connecté !"
      });
      }
  
    });

}

module.exports = {
  verifyToken,
  isAdmin
};