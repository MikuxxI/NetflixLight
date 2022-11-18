const db = require("../models");
const User = db.users;

const checkDuplicateUsernameOrEmail = (req, res, next) => {

    // Username
    User.findOne({ where: { username: req.body.username }}).then(user => { if (user) {

        res.status(400).send({
            message: "Un utilisateur avec le meme 'username' existe déjà !"
        });
        
        return;
    } else {

        res.status(200).send({
            message: "L'utilisateur est créé avec succes !"
        });

    }}).catch(err => {
        res.status(500).send({ message: err.message + "checkduplicateusername" });
      });

};

module.exports = {
    checkDuplicateUsernameOrEmail
};