/**
 * Autor: Heleno Sales Mesquita
 * Data : 01/08/2020
 * 
 *  Para iniciar basta executar node node-server.js
 */

"use strict";

const express = require("express");
const compression = require("compression");

const porta = 5003;
const distPath = 'C:\\lab\\tms-atomo\\Atm-Front\\dist\\atm-front';

const app = express();
app.use(compression());

app.get('*.*', express.static( distPath, {maxAge: '1y'} ));

app.all('*', function (req, res) {
    console.log(req)
    res.status(200).sendFile(`/`, { root: distPath } );
});

app.listen(porta, function () {
    console.log("Varejista sendo executado em http://localhost:" + porta );
});