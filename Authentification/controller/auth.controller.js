const db = require("../models");
const config = require("../config/auth.config");
const User = db.users;

const Op = db.Sequelize.Op;

var jwt = require("jsonwebtoken");
var crypto = require("crypto");
var bcrypt = require("bcryptjs");

exports.signup = (req, res) => {
    
    User.findOne({ where: { username: req.body.username }}).then(user => { 

        if (!user) {

            // Save User to Database
            User.create({ nom: req.body.nom, prenom: req.body.prenom, username: req.body.username, password: crypto.createHash('sha256').update(req.body.password).digest('hex'), solde: 0, roleAdmin: req.body.roleAdmin })
            .then( () => {

                res.send({ message: "User registered successfully!" });
            })
            .catch(err => {
            res.status(500).send({ message: err.message + "signup" });
            });

        } else {

            res.send({ user: user });
        }

    });
    
};

exports.signin = (req, res) => {

    User.findOne({
        where: {
        username: req.body.username,
        password: crypto.createHash('sha256').update(req.body.password).digest('hex')
        }
    })
    .then(user => {

        if (!user) {
            return res.status(404).send({ message: "User pas trouvé !" });
        }

        if (crypto.createHash('sha256').update(req.body.password).digest('hex').toString() != user.password.toString()) {

            return res.status(401).send({
            accessToken: null,
            message: "Mauvais mot de passe !"
            });
        }

        var token = jwt.sign({ id: user.ID }, config.secret, {
            expiresIn: 86400 // 24 hours
        });

        res.status(200).send({
            id: user.ID,
            nom: user.nom,
            prenom: user.prenom,
            username: user.username,
            solde: user.solde,
            roleAdmin: user.roleAdmin,
            accessToken: token
        });
    });
};