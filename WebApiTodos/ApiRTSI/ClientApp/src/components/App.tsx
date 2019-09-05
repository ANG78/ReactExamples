import * as React from 'react';
import { ITaskService } from '../interfaces/ITaskService';
import { ITask } from '../interfaces/ITask';
import { EnumStatusTask } from '../interfaces/EnumStatusTask';
import { EditTaskForm } from './EditTaskForm';
import { ContainerTask } from './ContainerTask';
import { TaskImp } from '../model/TaskImp';

interface IProps {
  taskService: ITaskService,
}

interface IState {
  tasksPending: ITask[];
  tasksCompleted: ITask[];
  isLoading: boolean;
  isError: boolean;
  isEditionMode: boolean;
}

export class App extends React.Component<IProps, IState>{

  constructor(props: IProps) {
    super(props);

    this.handleAddTask = this.handleAddTask.bind(this);
    this.handleCloseTask = this.handleCloseTask.bind(this);
    this.handleToggleStatus = this.handleToggleStatus.bind(this);
    this.handleOnDragAndDrop = this.handleOnDragAndDrop.bind(this);

    this.state = {
      tasksPending: [],
      tasksCompleted: [],
      isLoading: true,
      isError: false,
      isEditionMode: false,
      }

  }

  componentDidMount() {

    this.props.taskService.LoadTasks()
      .then((response) => {

        const completed = response.filter(x => x.status === EnumStatusTask.Completed);
        const pending = response.filter(x => x.status === EnumStatusTask.Pending)

        this.setState({
          tasksCompleted: completed,
          tasksPending: pending,
          isLoading: false
        });

      }).catch((error: Response) => {
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

    return this.props.taskService.AddTask(newTask)
      .then((response) => {

        const task = response;

        if (newTask.status === EnumStatusTask.Pending) {
          this.setState({
            tasksPending: [...this.state.tasksPending, task]
          });
        }
        else {
          this.setState({
            tasksCompleted: [...this.state.tasksCompleted, task]
          });
        }
      })
      .then();
  }

  handleToggleStatus(task: ITask): Promise<ITask> {

    const intialStatus = task.status;
    return this.props.taskService.ToggleStatus(task)
      .then((response) => {

        if (intialStatus === response.status) {
          alert("status not changed");
          return;
        }

        if (intialStatus === EnumStatusTask.Pending) {
          this.setState({
            tasksCompleted: [...this.state.tasksCompleted, response],
            tasksPending: this.state.tasksPending.filter((e) => { return e.id !== task.id; }),
          });

        }
        else {
          this.setState({
            tasksPending: [...this.state.tasksPending, response],
            tasksCompleted: this.state.tasksCompleted.filter((e) => { return e.id !== task.id; }),
          });
        }

      })
      .then();
  }

  handleOnDragAndDrop(idTask: number, status: EnumStatusTask): Promise<ITask> {

    console.log(idTask);
    console.log(status);

    const intialStatus = status;
    var taskFound: ITask = new TaskImp();
    var result = this.state.tasksPending.filter((e) => { return e.id === idTask; });

    if (result.length > 0) {
      taskFound = result[0]
    }
    else {
      result = this.state.tasksCompleted.filter((e) => { return e.id === idTask; });
      if (result.length > 0) {
        taskFound = result[0]
      }
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

        const containerPending = <ContainerTask
            status={EnumStatusTask.Pending}
            title="Pending Tasks"
            tasks={this.state.tasksPending}
            onToggleStatus={this.handleToggleStatus}
            onDragAndDrop={this.handleOnDragAndDrop}
        />;

        const containerCompleted = <ContainerTask
            status={EnumStatusTask.Completed}
            title="Tasks Completed"
            tasks={this.state.tasksCompleted}
            onToggleStatus={this.handleToggleStatus}
            onDragAndDrop={this.handleOnDragAndDrop}
        />;

            

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
                {!this.state.isError &&
                    !this.state.isLoading &&
                        containerPending
                };
                           
                </div>

                <div className="col-6">

                    {!this.state.isError &&
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

