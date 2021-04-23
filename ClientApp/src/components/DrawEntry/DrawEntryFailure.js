import React from 'react';
import { XCircle } from 'react-bootstrap-icons';

const DrawEntryFailure = (props) => {

  if (props.isVisible) {
    return (
      <div className="alert alert-danger" role="alert">
        <h4 className="alert-heading display-6"><XCircle className='text-danger'/> Something Went Wrong</h4>
        <p>Your entry was not submitted. Please try again in a moment!</p>
      </div>
    )
  }
  else {
    return <></>
  }
}

export default DrawEntryFailure;