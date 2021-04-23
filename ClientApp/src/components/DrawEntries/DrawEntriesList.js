import React, { Component } from "react";
import DrawEntriesListItem from "./DrawEntriesListItem";

function DrawEntriesList(props) {
  return (
    <table className="table table-hover table-sm">
      <caption>
        {props.items.length} {props.items.length === 1 ? "entry" : "entries"}
      </caption>
      <thead>
        <tr>
          <th scope="col">Ticket ID</th>
          <th scope="col">Name</th>
          <th scope="col">Prize</th>
        </tr>
      </thead>
      <tbody>
        {props.items.map((ticket) => (
          <DrawEntriesListItem ticket={ticket} />
        ))}
      </tbody>
    </table>
  );
}

export default DrawEntriesList;
