import React from "react";

function DrawEntriesListItem(props) {
  return (
    <tr>
      <th scope="row">{props.ticket.key}</th>
      <td>{props.ticket.name}</td>
      <td>{props.ticket.prize}</td>
    </tr>
  );
}

export default DrawEntriesListItem;
