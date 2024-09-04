import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-header',  
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HeaderComponent {
  sidebarVisible: boolean = false;
  dropdownVisible: boolean = false;
  userName = 'Heyler Montoya';

  logout(){
    this.dropdownVisible = false;
  }

  toggleDropdown(){
    this.dropdownVisible = !this.dropdownVisible;
  }

  toggleSidebar() {
    this.sidebarVisible = !this.sidebarVisible;
  }
}
