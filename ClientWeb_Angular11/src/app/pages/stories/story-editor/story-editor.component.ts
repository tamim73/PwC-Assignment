import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import {
  AddPostRequest,
  AddStoryRequest,
  EditPostRequest,
} from '../stories.DTO';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-story-editor',
  templateUrl: './story-editor.component.html',
  styleUrls: ['./story-editor.component.scss'],
})
export class StoryEditorComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private storiesService: StoriesService,
    private route: ActivatedRoute
  ) {}

  /** needed if edit mode */
  @Input() postId: number;
  /** needed when adding a post to existing story
   *  when (type = post and mode = add)
   */
  @Input() storyId: number;
  /** if not passed will check for storyId */
  @Input() type: 'story' | 'post';
  /** add for story and post, edit only for post */
  @Input() mode: 'add' | 'edit';

  @Output() submitted = new EventEmitter();

  // tinymce editor options
  editorOptions = {
    height: 500,
    menubar: false,
    plugins: [
      'advlist autolink lists link image charmap print preview anchor',
      'searchreplace visualblocks code fullscreen',
      'insertdatetime media table paste code help wordcount',
    ],
    toolbar:
      'undo redo | formatselect | bold italic backcolor | \
    alignleft aligncenter alignright alignjustify | \
    bullist numlist outdent indent | removeformat | help',
  };

  storyFG = this.fb.group({
    id: [''],
    title: ['', [Validators.required]],
    description: ['', []],
    content: ['', []],
  });

  id = +this.route.snapshot.paramMap.get('id');
  ngOnInit(): void {
    if (!this.type) {
      this.type = this.storyId ? 'post' : 'story';
    }
    if (!this.mode) {
      this.mode = this.id ? 'edit' : 'add';
    }
    if (this.mode === 'edit') {
      this.storiesService.getPost$(this.postId).subscribe((res) => {
        this.storyFG.patchValue({
          id: res.id,
          title: res.title,
          description: res.description,
          content: res.content,
        });
      });
    }
  }

  save(): void {
    console.log(this.storyFG.value);

    if (this.storyFG.invalid) {
      return;
    }

    const { id, title, description, content } = this.storyFG.value;

    if (this.mode === 'add') {
      if (this.type === 'story') {
        const req: AddStoryRequest = {
          title,
          content,
          description,
        };
        this.storiesService.addStory(req);
      }
      if (this.type === 'post') {
        const req: AddPostRequest = {
          title,
          content,
          description,
          storyId: this.storyId,
        };
        this.storiesService.addPost(req);
      }
    }

    if (this.mode === 'edit') {
      const req: EditPostRequest = {
        id,
        title,
        content,
        description,
      };
      this.storiesService.editPost(req);
    }

    this.submitted.emit();
  }
}
