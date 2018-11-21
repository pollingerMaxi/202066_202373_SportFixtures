import { Injectable } from "@angular/core";
import { CanActivate, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Observable } from "rxjs";
import { SessionService } from "src/app/services";
import { ToasterService } from "angular2-toaster";

@Injectable({
    providedIn: 'root'
})
export class AuthorizationGuard implements CanActivate, CanActivateChild {
    constructor(private sessionService: SessionService, private router: Router, private toasterService: ToasterService) { }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        return this.isAdmin(state.url);
    }

    canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
        return this.isAdmin(state.url);
    }

    isAdmin(url: string): boolean {
        if (this.sessionService.isAdmin()) { return true; }
        this.router.navigate(['/']);
        this.toasterService.pop("error", "Error!", "Must be admin");
        return false;
    }
}