const {gql} = require('apollo-server');

module.exports = gql`
type Query {
    sessions(id: ID
        title: String
        description: String
        startsAt: String
        endsAt: String
        room: Room
        day: String
        format: String
        track: String
        level: String): [Session],
    sessionById(id: ID): Session
    speakers: [Speaker]
    speakerById(id: ID): Speaker
}
enum Room {
    Europa
    Sol
    Saturn
}
type Mutation {
    toggleFavoriteSession(id: ID): Session
    addNewSession(session: SessionInput): Session
}
type Speaker {
    id: ID!
    bio: String
    name: String
    sessions: [Session]
}
input SessionInput {
    title: String!
    description: String
    startsAt: String
    endsAt: String
    room: String
    day: String
    format: String
    track: String
    level: String
    favorite: Boolean
} 
type Session {
    id: ID!
    title: String!
    description: String
    startsAt: String
    endsAt: String
    room: String
    day: String
    format: String
    track: String @deprecated(reason: "Too many sessions do not fit into a single track, we will be moving to a tag based system in the future")
    level: String
    favorite: Boolean
    speakers: [Speaker]
}`