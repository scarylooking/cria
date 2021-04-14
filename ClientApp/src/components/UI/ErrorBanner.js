import React from 'react';

const ErrorBanner = (props) => {

  if (props.isVisible) {
    return (
      <div className="row">
        <div className="col-lg-12">
          <div className="alert alert-danger" role="alert">Something went wrong. Please try again</div>
        </div>
      </div>
    )
  }
  else {
    return <></>
  }
}

export default ErrorBanner;