import { Component } from '@angular/core';

@Component({
  selector: 'app-users-management',
  templateUrl: './users-management.component.html'
})
export class UsersManagementComponent {
  activeTab: string = 'list';

  activateTab(tab: string) {
    this.activeTab = tab;
  }
}
