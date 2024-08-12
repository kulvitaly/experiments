const {ApolloServer, gql, ApolloError } = require('apollo-server');
const SessionAPI = require('./datasources/sessions');
const SpeakerAPI = require('./datasources/speakers');
const sessions = require('./data/sessions.json');
const typeDefs = require('./schema.js');
const resolvers = require('./resolvers.js');

const dataSources = () => ({
    sessionAPI: new SessionAPI(),
    speakerAPI: new SpeakerAPI(),
});

const server = new ApolloServer({ 
    typeDefs, 
    resolvers, 
    dataSources,
    //debug: true,
    formatError: (err) => {
        if (err.extensions.code == 'INTERNAL_SERVER_ERROR') {
            return new ApolloError('We are having some trouble', "ERROR", {token: 'uniquetoken'});
        }
    }
});

server
    .listen({port: process.env.PORT || 4000})
    .then(({url}) => {
        console.log(`GraphQL Server ready at ${url}`);
    });
