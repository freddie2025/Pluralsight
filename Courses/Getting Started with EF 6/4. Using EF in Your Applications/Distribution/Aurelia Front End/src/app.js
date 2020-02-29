
import {inject} from 'aurelia-framework';
import {Router} from 'aurelia-router';
import {HttpClient} from 'aurelia-http-client';
import moment from 'moment';
import 'bootstrap';
import 'bootstrap/css/bootstrap.css!';

@inject(Router, HttpClient)
export class App {
  constructor(router, client) {
this.router = router;
this.client = client;
 this.router.configure(config => {
    config.title = 'EF6 Getting Started';
    config.map([
    { route: ['', 'ninjas'],  moduleId: './ninjas', nav: true, title:'Ninjas' },
  { route: 'ninjas/*Id', moduleId: './edit', title:'Edit Ninja' },
  { route: 'insert',  moduleId: './insert', title:'Insert Ninja' }  
    ]);

 });
this.client.configure(x => {
//x.withBaseUrl('http://tutaurelia.azurewebsites.net/api');
x.withBaseUrl('http://localhost:46534/api');      
x.withHeader('accept', 'application/json')});
  }

}
