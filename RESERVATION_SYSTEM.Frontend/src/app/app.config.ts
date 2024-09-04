import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { initializeApp, provideFirebaseApp } from '@angular/fire/app';
import { getAuth, provideAuth } from '@angular/fire/auth';


export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes), 
    provideClientHydration(), 
    provideFirebaseApp(() => initializeApp(
      {"projectId":"loguin-3ae79",
      "appId":"1:763809996450:web:56bbf83cae14ae87acc1fb",
      "storageBucket":"loguin-3ae79.appspot.com",
      "apiKey":"AIzaSyD8HUYxexZO1dM7iKAZ-K1qUzHOxrcVZLg",
      "authDomain":"loguin-3ae79.firebaseapp.com",
      "messagingSenderId":"763809996450",
      "measurementId":"G-RZJ10SMTB3"})), 
    provideAuth(() => getAuth())
  ]
};
