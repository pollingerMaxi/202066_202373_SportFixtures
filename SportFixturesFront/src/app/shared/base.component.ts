import { OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

export abstract class BaseComponent implements OnInit {

    constructor(protected _route: ActivatedRoute) {
        
    }

    ngOnInit() {

    }
}