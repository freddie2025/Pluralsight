import {
    inject
}
from 'aurelia-framework'
import {
    HttpClient
}
from 'aurelia-http-client';
import {
    Router
}
from 'aurelia-router';

@
inject(HttpClient, Router)

export class Edit {
    searchEntry = '';
    ninjas = [];
    ninjaId = '';
    ninja = '';
    ninjaRoot = '';
    currentPage = 1;
    textShowAll = 'Show All';
    myRouter = '';

    constructor(http, router) {
        this.http = http;
        this.myRouter = router;
    }

    retrieveNinja(id) {
        return this.http.createRequest("/ninjas/" + id)
            .asGet().send().then(response => {
                this.ninja = response.content;
            });

    }

    save() {
        this.ninjaRoot = {
            Id: this.ninja.Id,
            ServedInOniwaban: this.ninja.ServedInOniwaban,
            ClanId: this.ninja.ClanId,
            Name: this.ninja.Name,
            DateOfBirth: this.ninja.DateOfBirth,
            DateCreated: this.ninja.DateCreated,
            DateModified: this.ninja.DateModified
        };
        this.http.createRequest("/ninjas/")
            .asPost()
            .withHeader('Content-Type', 'application/json; charset=utf-8')
            .withContent(this.ninjaRoot).send()
            .then(response => {
                this.myRouter.navigate('ninjas');
            }).catch(err => {
                console.log(err);
            });

    }



    get canSearch() {
        return (this.searchEntry != '' ? true : false);
    }

    activate(params) {
        var result = this.retrieveNinja(params.Id);
        return result;
    }

}
