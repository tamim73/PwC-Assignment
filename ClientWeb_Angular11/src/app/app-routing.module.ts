import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GuestGuard } from './core/guards/guest.guard';
import { PagesComponent } from './pages/pages.component';
import { StoriesListComponent } from './pages/stories/stories-list/stories-list.component';
import { StoryEditorComponent } from './pages/stories/story-editor/story-editor.component';
import { StoryViewComponent } from './pages/stories/story-view/story-view.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('src/app/auth/auth.module').then((m) => m.AuthModule),
    canActivate: [GuestGuard],
  },
  {
    path: 'stories',
    component: PagesComponent,
    children: [
      {
        path: '',
        component: StoriesListComponent,
      },
      {
        path: 'add',
        component: StoryEditorComponent,
      },
      {
        path: 'edit/:id',
        component: StoryEditorComponent,
      },
      {
        path: 'view/:id',
        component: StoryViewComponent,
      },
      
    ],
  },
  {
    path: '',
    redirectTo: 'stories',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
