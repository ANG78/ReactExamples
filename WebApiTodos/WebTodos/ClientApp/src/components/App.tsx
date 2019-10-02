import * as React from 'react';
import { ITaskService } from '../interfaces/ITaskService';
import { ITask } from '../interfaces/ITask';
import { EnumStatusTask } from '../interfaces/EnumStatusTask';
import { EditTaskForm } from './EditTaskForm';
import { ContainerTask } from './ContainerTask';
import { TaskServiceDummy } from './../services/TaskServiceDummy';
import { TaskService } from './../services/TaskService'

interface IProps {
}

interface IState {
  tasks: ITask[];
  isLoading: boolean;
  isError: boolean;
  isEditionMode: boolean;
}

export class App extends React.Component<IProps, IState>{

  isDummy: boolean = false;
  service: ITaskService = this.isDummy ? new TaskServiceDummy() : new TaskService();


  constructor(props: IProps) {
    super(props);
      
    this.handleAddTask = this.handleAddTask.bind(this);
    this.handleCloseTask = this.handleCloseTask.bind(this);
    this.handleToggleStatus = this.handleToggleStatus.bind(this);
    this.handleOnDragAndDrop = this.handleOnDragAndDrop.bind(this);

    this.state = {
      tasks: [],
      isLoading: true,
      isError: false,
      isEditionMode: false,
    }

  }

  componentDidMount() {

      this.service.LoadTasks()
      .then((response) => {

        this.setState({
          tasks: response,
          isLoading: false
        });

      }).catch((error: Response) => {

        //console.log(error);
        error.text()
          .then((data: string) => {
            alert(data);
            this.setState({
              isError: true,
              isLoading: false
            });
          })
      });
  }

  handleCloseTask(): any {
    this.setState({
      isEditionMode: false
    });
  }

  handleAddTask(newTask: ITask): Promise<ITask> {

    return this.service.AddTask(newTask)
      .then((response) => {

        const task = response;

        if (newTask.status === EnumStatusTask.Pending) {
          var pending: ContainerTask = this.refs.PendingContainer as ContainerTask;
          pending.add(task);
        }
        else {
          var completed: ContainerTask = this.refs.CompletedContainer as ContainerTask;
          completed.add(task);
        }

      })
      .then();
  }

  handleToggleStatus(task: ITask): Promise<ITask> {

    const intialStatus = task.status;
    return this.service.ToggleStatus(task)
      .then((response) => {

        if (intialStatus === response.status) {
          alert("status not changed");
          return;
        }


        var pending: ContainerTask = this.refs.PendingContainer as ContainerTask;
        var completed: ContainerTask = this.refs.CompletedContainer as ContainerTask;

        if (intialStatus === EnumStatusTask.Pending) {
          completed.add(response);
          pending.remove(task);
        }
        else {
          pending.add(response);
          completed.remove(task);
        }

      })
      .then();
  }

  handleOnDragAndDrop(idTask: number, status: EnumStatusTask): Promise<ITask> {

    const intialStatus = status;

    var pending: ContainerTask = this.refs.PendingContainer as ContainerTask;
    var completed: ContainerTask = this.refs.CompletedContainer as ContainerTask;

    var taskFound = pending.get(idTask);
    if (taskFound.id === 0) {
      taskFound = completed.get(idTask);
    }

    if (taskFound.id > 0 && taskFound.status !== intialStatus) {
      const taskToChange = taskFound;
      return this.handleToggleStatus(taskToChange);
    }

    return new Promise((e) => {
      return taskFound;
    });
  }

  handleEdition(e: React.MouseEvent<HTMLButtonElement, MouseEvent>) {
    e.preventDefault();
    this.setState({
      isEditionMode: !this.state.isEditionMode
    });
  }

  render() {

    const containerPending= <ContainerTask
      ref="PendingContainer"
      status={EnumStatusTask.Pending}
      title="Pending Tasks"
      tasks={this.state.tasks}
      onToggleStatus={this.handleToggleStatus}
      onDragAndDrop={this.handleOnDragAndDrop}
      />

    const containerCompleted=<ContainerTask
      ref="CompletedContainer"
      status={EnumStatusTask.Completed}
      title="Tasks Completed"
      tasks={this.state.tasks}
      onToggleStatus={this.handleToggleStatus}
      onDragAndDrop={this.handleOnDragAndDrop}
     />

    return (
      <div className="App col-12">
        <header className="App-header">

          <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <button className="navbar-brand"
              onClick={(e) => this.handleEdition(e)}>
              CREATE
            </button>
            <a className="navbar-brand"
              href="/"
            >
              RELOAD
            </a>
          </nav>
        </header>

        <div className="editTaskForm">
          {!this.state.isError &&
            this.state.isEditionMode &&
            <div>
              <EditTaskForm
                onCloseEdition={this.handleCloseTask}
                onAddTask={this.handleAddTask} />
            </div>
          }
        </div>

        <div className="container" >

          <div className="row">

            <div className="col-6">
              {
                !this.state.isError &&
                !this.state.isLoading &&
                containerPending
              }
            </div>

            <div className="col-6">
              {
                !this.state.isError &&
                !this.state.isLoading &&
                containerCompleted
              }
            </div>

          </div>

        </div>

      </div>
    );
  }

}
