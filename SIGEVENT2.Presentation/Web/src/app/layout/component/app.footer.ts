import { Component } from '@angular/core';

@Component({
    standalone: true,
    selector: 'app-footer',
    template: `<div class="layout-footer">
        SIGEVENT by
        <a href="http://www.eti.cu/" target="_blank" rel="noopener noreferrer" class="text-primary font-bold hover:underline">ETI</a>
    </div>`
})
export class AppFooter {}
