import React from "react";
import { AppConfiguration } from "read-appsettings-json";
import { getJwt } from "../../Helpers";

export class CreateEvent extends React.Component {
    constructor(props) {
        super(props);

        this.handleSubmit = this.handleSubmit.bind(this);

        const now = new Date(Date.now()).toISOString().slice(0, 16);

        this.state = {
            id: 0,
            name: "",
            description: "",
            startDate: now,
            endDate: now,
            imageUrl: "",
            layoutGroups: [],
            layoutId: -1,
        }
    }

    async componentDidMount() {
        const response = await fetch("/event/getAvailableLayouts");
        const layoutGroups = await response.json();
        this.setState({ layoutGroups: layoutGroups });
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

        await fetch(AppConfiguration.Setting().CreateEventUrl, {
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
                <label for="LayoutId">Layout Id</label>
                <select name="LayoutId" value={this.state.layoutId} onChange={(e) => this.setState({ layoutId: e.target.value })}>
                    {this.state.layoutGroups.map(layoutGroup => {
                        return (
                            <optgroup key={layoutGroup.venue.id} label={layoutGroup.venue.address}>
                                {layoutGroup.layouts.map(layout => {
                                    return <option key={layout.id} value={layout.id}>{layout.description}</option>
                                })}
                            </optgroup>
                        );
                    })}
                </select>
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
                    <input type="submit" value="Create" />
                </div>
            </form>
        );
    }
}