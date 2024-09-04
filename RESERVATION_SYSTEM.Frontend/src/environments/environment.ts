import { api } from "./api.constants";
import { YARP_CEIBA_XM } from "./environment.constants";

export const environment = {
    production: false,
    endpoint_api_customer: `${YARP_CEIBA_XM}${api.custommer}`,
    endpoint_api_service: `${YARP_CEIBA_XM}${api.service}`,
    endpoint_api_reservation: `${YARP_CEIBA_XM}${api.reservation}`,
    endpoint_api_history_reservation: `${YARP_CEIBA_XM}${api.history_reservation}`,

    firebaseConfig: {
        apiKey: "AIzaSyD8HUYxexZO1dM7iKAZ-K1qUzHOxrcVZLg",
        authDomain: "loguin-3ae79.firebaseapp.com",
        projectId: "loguin-3ae79",
        storageBucket: "loguin-3ae79.appspot.com",
        messagingSenderId: "763809996450",
        appId: "1:763809996450:web:56bbf83cae14ae87acc1fb",
        measurementId: "G-RZJ10SMTB3"
    }
};
