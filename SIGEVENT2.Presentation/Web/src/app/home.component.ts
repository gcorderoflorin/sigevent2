import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-home',
    standalone: true,
    template: ` <div class="card">
        <div class="font-semibold text-xl mb-4">Empty Page</div>
    </div>`
})
export class HomeComponent implements OnInit {
    private readonly router = inject(Router);
    private readonly route = inject(ActivatedRoute);


    ngOnInit() {
        const jwt = localStorage.getItem('jwt');
        if(jwt == null) {
            this.router.navigate(["login"], { relativeTo: this.route });
        } else {
            this.router.navigate(["landing"], { relativeTo: this.route });
        }
    }
}