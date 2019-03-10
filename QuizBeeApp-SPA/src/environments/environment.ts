// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,

  //LOCAL
  apiUrl:"http://localhost:5000/api/",
  apiUrlRoot:"http://localhost:5000/"


  // FAITH
  // apiUrl:"http://172.16.123.183:5000/api/",
  // apiUrlRoot:"http://172.16.123.183:5000/"


  //HOME
  // apiUrl:"http://192.168.100.65:5000/api/",
  // apiUrlRoot:"http://192.168.100.65:5000/"

  //DRAKE
  // apiUrl:"http://10.40.1.119:5000/api/",
  // apiUrlRoot:"http://10.40.1.119:5000/"
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
