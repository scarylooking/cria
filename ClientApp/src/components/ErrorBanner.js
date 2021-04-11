import React from 'react';

const ErrorBanner = props => {
  let { isError } = props;

  if (isError) {
    return (
      <div className="alert alert-danger" role="alert">
        Something went wrong. Please try again.
      </div>
    )
  }
  else {
    return <></>
  }
}

export default ErrorBanner;