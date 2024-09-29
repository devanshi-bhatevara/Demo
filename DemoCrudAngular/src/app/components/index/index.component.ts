import { Component } from '@angular/core';
import { ApiResponse } from '../../models/ApiResponse{T}';
import { Demo } from '../../models/demo.model';
import { DemoService } from '../../services/demo.service';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-index',
  standalone: true,
  imports: [RouterOutlet, CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './index.component.html',
  styleUrl: './index.component.css'
})
export class IndexComponent {
  demos: Demo[] | undefined |null;

  constructor(private demoService: DemoService, private router: Router) {    
  }
  ngOnInit(): void {
    this.loadList();
 }

 loadList():void{
  this.demoService.getAll().subscribe({
    next:(response: ApiResponse<Demo[]>) =>{
      if(response.success){
        this.demos = response.data;
        console.log(response.data);
      }
      else{
        console.error('Failed to fetch', response.message);
      }
    },
    error:(error => {
      console.error('Failed to fetch', error);
    })
  });
}

delete(id: number) {
  if (confirm('Are you sure you want to delete?')) {
    this.demoService.delete(id).subscribe(() => {
      this.loadList();
    });
  }
}

edit(id:number)
{
  this.router.navigate(['/edit', id]);
}
}
