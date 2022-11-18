//REQUIREMENTS
const db = require("../models");
const amqp = require("amqplib");
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
            .then( async () => {

                res.send({ message: "User registered successfully!" });

                //RabbitMQ
                const connectionDamien = await amqp.connect("amqp://guest:guest@10.111.21.78:5672");
                const channel = await connectionDamien.createChannel();
                await channel.assertQueue("ms.auth.queu.register");
                channel.sendToQueue("ms.auth.queu.register", Buffer.from("Création_Nouveau_Compte_OK"));
            })
            .catch(async err => {

                res.status(500).send({ message: err.message + "signup" });

                //RabbitMQ
                const connectionDamien = await amqp.connect("amqp://guest:guest@10.111.21.78:5672");
                const channel = await connectionDamien.createChannel();
                await channel.assertQueue("Création_Nouveau_Compte_KO");
                channel.sendToQueue("ms.auth.queu.register", Buffer.from("Création_Nouveau_Compte_KO"));
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
    .then(async user => {

        if (!user) {

            //RabbitMQ
            const connectionDamien = await amqp.connect("amqp://guest:guest@10.111.21.78:5672");
            const channel = await connectionDamien.createChannel();
            await channel.assertQueue("Connexion_Compte_KO");
            channel.sendToQueue("ms.auth.queu.login", Buffer.from("Connexion_Compte_KO"));

            return res.status(404).send({ message: "User pas trouvé !" });
        }

        if (crypto.createHash('sha256').update(req.body.password).digest('hex').toString() != user.password.toString()) {

            //RabbitMQ
            const connectionDamien = await amqp.connect("amqp://guest:guest@10.111.21.78:5672");
            const channel = await connectionDamien.createChannel();
            await channel.assertQueue("Connexion_Compte_KO");
            channel.sendToQueue("ms.auth.queu.login", Buffer.from("Connexion_Compte_KO"));

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

        //RabbitMQ
        const connectionDamien = await amqp.connect("amqp://guest:guest@10.111.21.78:5672");
        const channel = await connectionDamien.createChannel();
        await channel.assertQueue("Connexion_Compte_OK");
        // channel.consume("Connexion_Compte_OK", message => {
        //     const input = JSON.parse(message.content.toString());
        //     console.log(`Received Connexion_Compte_OK: ${input.number}`);
        //     channel.ack(message);
        // });
        channel.sendToQueue("ms.auth.queu.login", Buffer.from("Connexion_Compte_OK"));
    });
};