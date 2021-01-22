import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GuestGuard } from './core/guards/guest.guard';
import { PagesComponent } from './pages/pages.component';
import { StoriesListComponent } from './pages/stories/stories-list/stories-list.component';
import { StoryDetailsComponent } from './pages/stories/story-details/story-details.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('src/app/auth/auth.module').then((m) => m.AuthModule),
    canActivate: [GuestGuard],
  },
  {
    path: '',
    component: PagesComponent,
    children: [
      {
        path: 'stories',
        component: StoriesListComponent
      },
      {
        path: 'stories/:id',
        component: StoryDetailsComponent
      },
      {
        path: '**',
        redirectTo: 'stories',
      }
    ],
  },

  // ----------
  {
    path: '**',
    redirectTo: '',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
