import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllblogsComponent } from './components/allblogs/allblogs.component';
import { BlogComponent } from './components/add-update-view/blog.component';
import { AuthGuard } from './services/auth-guard.service';


const appRoutes: Routes = [
      {
        path: '',
        component: AllblogsComponent
      },
      { path: 'blog/add',
       component: BlogComponent
      },
      { path: 'blog/edit/:id',
      component: BlogComponent
      },
      { path: 'blog/details/:id',
      component: BlogComponent
      }
]

@NgModule({
    imports: [
      // RouterModule.forRoot(appRoutes, { useHash: true }),
      RouterModule.forRoot(appRoutes)
    ],
    exports: [RouterModule]
  })
  export class AppRoutingModule {
  
  }