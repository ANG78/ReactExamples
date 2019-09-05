import React, { Component } from 'react';
import { Task } from './Task';
import { ITask } from '../interfaces/ITask';
import { EnumStatusTask } from '../interfaces/EnumStatusTask';

interface IProps {
  title: String,
  tasks: ITask[],
  status: EnumStatusTask,
  onToggleStatus: (task: ITask) => Promise<ITask>
  onDragAndDrop: (idTask:number, status:EnumStatusTask) => Promise<ITask>
}

interface State {
}

export class ContainerTask extends Component<IProps, State> {

  handlerOnDragOver(ev: React.DragEvent<HTMLDivElement>)
  {
    ev.preventDefault();
  }

  hadlerOnDropEnd(ev :React.DragEvent<HTMLDivElement>)
  {
    var idTask = parseInt(ev.dataTransfer.getData("text/plain"));  
    this.props.onDragAndDrop(idTask,this.props.status)
      .then((resul)=>{
        //nothing
      })
      .catch((resul)=>{
        resul.text().then( (data:string) => { alert(data);})
      });
    ev.preventDefault();
  }

  render() {
    return (
        <div className={"droppable mt-2"}
        onDragOver={(e) => this.handlerOnDragOver(e)}
        onDrop={(e) => this.hadlerOnDropEnd(e) }
      >
        <div className="container-drag containertask">
          <div className="row" >
            <div className="card col-12">
                <h4 className="card-body"> {this.props.title}</h4>
            </div>
            <div className="col-12 mt-1" >
              {this.props.tasks.map((taskIterator, i) => {
                 return <Task
                  key={taskIterator.id}
                  task={taskIterator}
                  onToggleStatus={this.props.onToggleStatus} />
                 })}
            </div>
          </div>
        </div>
      </div>
    );
  }

 
}
