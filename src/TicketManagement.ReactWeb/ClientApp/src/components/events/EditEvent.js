import React from "react";
import DateTimePicker from "react-datetime-picker/dist/DateTimePicker";
import { Redirect } from "react-router-dom";
import { Form } from "reactstrap";
import { AppConfiguration } from "read-appsettings-json";
import { getJwt } from "../../Helpers";

export class EditEvent extends React.Component {
    constructor(props) {
        super(props);

        this.handleSubmit = this.handleSubmit.bind(this);

        const event = props.location.event;

        this.state = {
            id: event.id,
            layoutId: event.layoutId,
            name: event.name,
            description: event.description,
            startDate: event.startDate,
            endDate: event.endDate,
            imageUrl: event.imageUrl,
        }
    }

    async handleSubmit(e) {
        e.preventDefault();

        const formData = new FormData();

        formData.append("id", this.state.id);
        formData.append("layoutId", this.state.layoutId);
        formData.append("name", this.state.name);
        formData.append("description", this.state.description);
        formData.append("startDate", this.state.startDate);
        formData.append("endDate", this.state.endDate);
        formData.append("imageUrl", this.state.imageUrl);

        await fetch(AppConfiguration.Setting().UpdateEventUrl, {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + getJwt(),
            },
            body: formData,
        })

        this.props.history.push("/events/getall");
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <div>
                    <label name="Name">Name</label>
                    <input
                        type="text"
                        name="Name"
                        value={this.state.name}
                        onChange={(e) => this.setState({
                            name: e.target.value,
                        })}
                    />
                </div>
                <div>
                    <label name="Description">Description</label>
                    <input
                        type="text"
                        name="Description"
                        value={this.state.description}  
                        onChange={(e) => this.setState({
                            description: e.target.value,
                        })}
                    />
                </div>
                <div>
                    <label name="StartDate">Start date</label>
                    <input
                        type="datetime-local"
                        name="StartDate"
                        value={this.state.startDate}
                        onChange={(e) => this.setState({
                            startDate: e.target.value,
                        })}
                    />
                </div>
                <div>
                    <label name="EndDate">End date</label>
                    <input
                        type="datetime-local"
                        name="EndDate"
                        value={this.state.endDate}
                        onChange={(e) => this.setState({
                            endDate: e.target.value,
                        })}
                    />
                </div>
                <div>
                    <label name="ImageUrl">Image Url</label>
                    <input
                        type="text"
                        name="ImageUrl"
                        value={this.state.imageUrl}
                        onChange={(e) => this.setState({
                            imageUrl: e.target.value,
                        })}
                    />
                </div>
                <div>
                    <input type="submit" value="Edit" />
                </div>
            </form>
        );
    }
}