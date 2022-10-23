import React from "react";
import { Link } from "react-router-dom";
import { AppConfiguration } from "read-appsettings-json";
import { getRoles } from "../../Helpers";
import { EventRow } from "./EventRow";

export class EventsTable extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            events: [],
            roles: [],
        }
    }

    async getEvents() {
        const response = await fetch(AppConfiguration.Setting().GetEventsUrl);

        if (response.ok) {
            return await response.json();
        }

        return [];
    }

    componentDidMount() {
        this.timerID = setInterval(async () => {
            const events = await this.getEvents();
            const roles = await getRoles();
            
            this.setState({
                events: events,
                roles: roles,
            });
        }, 1000);
    }

    componentWillUnmount() {
        clearInterval(this.timerID);
    }

    render() {
        const isManager = this.state.roles.includes("Manager");
        const isAdmin = this.state.roles.includes("Admin");

        let actionsTableHeaderElement;

        if (isManager || isAdmin) {
            actionsTableHeaderElement = (<th>Actions</th>);
        }

        let createEventLinkElement;

        if (isAdmin) {
            createEventLinkElement = <Link to="/events/create">Create event</Link>
        }

        return (
            <div>
                {createEventLinkElement}
                <table className="table">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Description</th>
                            <th>Start date</th>
                            <th>End date</th>
                            <th>Image</th>
                            <th>More info</th>
                            {actionsTableHeaderElement}
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.events.map(event => {
                            return <EventRow key={event.id} event={event} isEditable={isManager || isAdmin} />
                        })}
                    </tbody>
                </table>
            </div>
        );
    }
}