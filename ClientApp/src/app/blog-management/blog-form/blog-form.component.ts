import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Blog } from 'src/app/_models/blog-model';
import { BlogService } from 'src/app/_services/blog.service';
import { ToastrService } from 'ngx-toastr';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { environment } from 'src/environments/environment';
import { TypeBlogService } from 'src/app/_services/type-blog.service';
import { TypeBlog } from 'src/app/_models/type-blog-model';

@Component({
  selector: 'app-blog-form',
  templateUrl: './blog-form.component.html'
})
export class BlogFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  img: any;
  public blog: Blog = new Blog();
  configEditor: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: '',
    translate: 'no',
    defaultParagraphSeparator: '',
    defaultFontName: 'Arial',
    toolbarHiddenButtons: [
      [],
      ['toggleEditorMode', 'removeFormat']
    ]
  };
  public files: any = [];
  public blogsType: TypeBlog[] = [];

  public isEdit = false;
  public isView = false;
  public isNew = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private blogService: BlogService,
    private typeBlogService: TypeBlogService

  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.blog.id = Number(params.id);
        this.load();
      }
    });

    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required, Validators.maxLength(255)]],
      details: [''],
      typeBlogId: ['', [Validators.required]],
    });
    this.typeBlogService.getAll().subscribe(result => {
      this.blogsType = result;
  
    });
  
    
    this.load();

  }



  load() {
    if (this.blog.id > 0) {
      this.blogService.getById(this.blog.id).subscribe(result => {
        this.blog = result;
        this.formAdd.controls.id.setValue(this.blog.id);
        this.formAdd.controls.name.setValue(this.blog.description);
        this.formAdd.controls.details.setValue(this.blog.details);
        this.img = environment.urlImagesLojas + this.blog.imageName;
        this.formAdd.controls.typeBlogId.setValue(this.blogsType.find(x => x.id === this.blog.typeBlogId));

      });
    }

  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const formData = new FormData();
    const item = new Blog();
    item.id = this.blog.id;
    item.details = this.formAdd.controls.details.value;
    item.description = this.formAdd.controls.name.value;
    item.typeBlogId = this.formAdd.controls.typeBlogId.value.id;

    formData.append('blog', JSON.stringify(item));
    if(this.files.length > 0) {
      this.files.forEach(f => {
        formData.append('file', f.file, f.file.name);
    });
    }

    this.blogService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/blog']);
    });
  }

  onCancel() {
    this.router.navigate([`partner-area/blog`]);
  }

  getImage(nomeImage) {
    return environment.urlImagesLojas + nomeImage;
  }
  
  canEdit() {
    return this.isEdit;
  }
  
  canView() {
    return this.isView;
  }
  
  canSave() {
    return this.isNew;
  }
  
  onFileChange(event) {
    if (event.target.files.length > 4) {
    return this.toastr.error('Só é permitido anexar um arquivo!');
    }
    
    if (event.target.files.length > 0) {
      this.onResetFileChange();
      this.files = [];
      for (const file of event.target.files) {
        this.files.push({ file });
      }
    }
  }

  onResetFileChange() {
    this.files = [];
}

}

