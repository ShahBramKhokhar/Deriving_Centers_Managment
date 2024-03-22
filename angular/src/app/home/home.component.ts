import { Router } from '@angular/router';
import { DashboardServiceProxy, DashboardDto } from './../../shared/service-proxies/service-proxies';
import { Component, Injector, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
  templateUrl: './home.component.html',
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent extends AppComponentBase implements OnInit {
  dashboardData: DashboardDto;
  constructor(injector: Injector,private dashboardService: DashboardServiceProxy,private router:Router) {
    super(injector);
  }

  ngOnInit(): void {
    this.dashboardService.getDashboardData().subscribe(response => {
      this.dashboardData = response;
    });
  }

  moveToCenters(){
    this.router.navigate(['app/centres']);
  }

  moveToTeachers(){
    this.router.navigate(['app/teachers']);
  }

}
