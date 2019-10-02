import React, { Component } from 'react';
import { Task } from './Task';
import { ITask } from '../interfaces/ITask';
import { EnumStatusTask } from '../interfaces/EnumStatusTask';
import { TaskImp } from '../model/TaskImp';

interface IProps {
  title: String,
  tasks: ITask[],
  status: EnumStatusTask,
  onToggleStatus: (task: ITask) => Promise<ITask>
  onDragAndDrop: (idTask: number, status: EnumStatusTask) => Promise<ITask>
}

interface State {
  tasks: ITask[]
}

export class ContainerTask extends Component<IProps, State> {

  public status: EnumStatusTask = EnumStatusTask.Undefined;

  constructor(props: IProps) {
    super(props);
    this.status = props.status;
    this.state = {
      tasks: []
    }
  }

  componentDidMount() {

    if (this.props.tasks !== null && this.props.tasks !== undefined) {
      const tasksFiltered = this.props.tasks.filter(x => x.status === this.status);
      this.setState(
        {
          tasks: tasksFiltered
        });
    }

  }

  handlerOnDragOver(ev: React.DragEvent<HTMLDivElement>) {
    ev.preventDefault();
  }

  hadlerOnDropEnd(ev: React.DragEvent<HTMLDivElement>) {
    var idTask = parseInt(ev.dataTransfer.getData("text/plain"));
    this.props.onDragAndDrop(idTask, this.props.status)
      .then((resul) => {
        //nothing
      })
      .catch((resul) => {
        resul.text().then((data: string) => { alert(data); })
      });
    ev.preventDefault();
  }

  render() {
    return (
      <div className={"droppable mt-2"}
        onDragOver={(e) => this.handlerOnDragOver(e)}
        onDrop={(e) => this.hadlerOnDropEnd(e)}
      >
        <div className="container-drag containertask">
          <div className="row" >
            <div className="card col-12">
              <h4 className="card-body"> {this.props.title}</h4>
            </div>

            <div className="col-12 mt-1" >
              {this.state.tasks !== null && this.state.tasks.map((taskIterator, i) => {
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

  public add(task:ITask) {
    this.setState({
      tasks: [...this.state.tasks, task]
    });
  }

  public remove(task:ITask) {
    this.setState({
      tasks: this.state.tasks.filter((e) => { return e.id !== task.id; })
    });
  }

  public get(idTask:number): ITask{

    var taskFound: ITask = new TaskImp();
    var result = this.state.tasks.filter((e) => { return e.id === idTask; });
    if (result.length > 0) {
      taskFound = result[0]
    }
    return taskFound;
  }
}
