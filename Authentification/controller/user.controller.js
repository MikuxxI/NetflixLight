exports.allAccess = (req, res) => {
    res.status(200).send("Accessible à tous");
};

exports.userBoard = (req, res) => {
    res.status(200).send("Accessible aux utilisateurs");
};

exports.adminBoard = (req, res) => {
    res.status(200).send("Accessible aux admins");
};