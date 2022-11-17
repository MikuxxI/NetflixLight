module.exports = (sequelize, Sequelize) => {
  
    const User = sequelize.define("users", {
        
        ID: 
        {
            type: Sequelize.INTEGER,
            allowNull: false,
            autoIncrement: true,
            primaryKey: true
        },
    
        nom: 
        {
            type: Sequelize.STRING(255),
            allowNull: false
        },
    
        prenom: 
        {
            type: Sequelize.STRING(255),
            allowNull: false
        },
    
        username: 
        {
            type: Sequelize.STRING(255),
            allowNull: false
        },

        password: 
        {
            type: Sequelize.STRING(255),
            allowNull: false
        },

        solde: 
        {
            type: Sequelize.INTEGER,
            allowNull: false,
            defaultValue: 0,
        },

        roleAdmin: 
        {
            type: Sequelize.BOOLEAN
        }
  
    }, { timestamps: false });
   
    
    return User;
  
  };