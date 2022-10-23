import React from "react";
import { AreaRow } from "./AreaRow";

export function AreasTable(props) {
    return (
        <table className="table">
            <thead>
                <tr>
                    <th>Description</th>
                    <th>Coord X</th>
                    <th>Coord Y</th>
                    <th>Price</th>
                    <th>More info</th>
                </tr>
            </thead>
            <tbody>
                {props.location.areas.map(area => <AreaRow key={area.id} area={area} />)}
            </tbody>
        </table>
    );
}