import {inject} from 'aurelia-framework'
import {HttpClient} from 'aurelia-http-client';
 
@inject(HttpClient)
export class Ninja {
  searchEntry = '';
  ninjas = [];
  ninjaId = '';
  ninja = '';
  currentPage = 1;
  textShowAll = 'Show All';
 
  constructor(http) {
    this.http = http;
  }

  retrieveNinjas() {
  	return this.http.createRequest
              ("/ninjas/?page=" + this.currentPage + "&pageSize=100&query=" + this.searchEntry)
      .asGet().send().then(response => {
        this.ninjas = response.content;
      });
  }
 
  get canSearch() {
    return (this.searchEntry != '' ? true : false);
  }
 
  activate() {
    return this.retrieveNinjas();
  }
 
  retrieveAllNinjas() {
    this.searchEntry = '';
    this.activate();
  }
}