import { StudentDetailComponent } from './students/student-detail/student-detail.component';
import { FeesDetailComponent } from './fees-records/fees-detail/fees-detail.component';
import { CentresComponent } from './centres/centres.component';
import { Component, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { TeachersComponent } from './teachers/teachers.component';
import { CenterDetailsComponent } from './centres/center-details/center-details.component';
import { ClassesAreaComponent } from './classes-area/classes-area.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'about', component: AboutComponent, canActivate: [AppRouteGuard] },
                    { path: 'update-password', component: ChangePasswordComponent, canActivate: [AppRouteGuard] },
                    { path: 'centres', component: CentresComponent, canActivate: [AppRouteGuard]  },
                    { path: 'center-detail', component: CenterDetailsComponent, canActivate: [AppRouteGuard]  },
                    { path: 'teachers', component: TeachersComponent, canActivate: [AppRouteGuard]},
                    { path: 'classes-area', component: ClassesAreaComponent, canActivate: [AppRouteGuard] },
                    { path: 'fees-detail', component: FeesDetailComponent, canActivate: [AppRouteGuard] },
                    { path: 'student-detail', component: StudentDetailComponent, canActivate: [AppRouteGuard] }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
