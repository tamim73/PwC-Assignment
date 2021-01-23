import { Component, Input, OnInit } from '@angular/core';
import { IPostBase } from '../../stories.DTO';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
})
export class PostComponent implements OnInit {
  constructor() {}

  @Input() post: IPostBase;

  ngOnInit(): void {}
}
