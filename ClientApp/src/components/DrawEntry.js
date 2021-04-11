import React, { Component } from 'react';
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from 'yup';
import ErrorBanner from "./ErrorBanner";

export class DrawForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isError: false
    };
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleError = this.handleError.bind(this);
  }

  handleError(){
    this.setState({isError: true})
  }

  handleSubmit(values, actions) {
    fetch('/api/drawEntry', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(values)
    }).then(response => {
      actions.setSubmitting(false);
      return response.json();
    }).then(data => {
      console.debug('response', data)
    });
  }

  render() {
    return (
      <div className="container">

        <div className="row mb-5">
          <div className="col-lg-12 text-center">
            <h1 className="mt-5">Login Form</h1>
          </div>
        </div>

        <ErrorBanner isError={this.state.isError} />

        <div className="row">
          <div className="col-lg-12">
            <Formik
              initialValues={{
                email: "",
                name: "",
                prize: "any"
              }}
              validationSchema={
                Yup.object({
                  name: Yup
                    .string()
                    .required('Name is required'),
                  email: Yup
                    .string()
                    .email('Invalid email address')
                    .required('Email is required'),
                })}
              onSubmit={this.handleSubmit}
            >
              {({ touched, errors, isSubmitting }) => (
                <Form>
                  <div className="form-group">
                    <label htmlFor="name">Name</label>
                    <Field
                      type="text"
                      name="name"
                      placeholder="Al Paca"
                      className={`form-control ${touched.name && errors.name ? "is-invalid" : ""
                        }`}
                    />
                    <ErrorMessage
                      component="div"
                      name="name"
                      className="invalid-feedback"
                    />
                  </div>

                  <div className="form-group">
                    <label htmlFor="email">Email</label>
                    <Field
                      type="email"
                      name="email"
                      placeholder="al.paca@dotnetnorth.org.uk"
                      className={`form-control ${touched.email && errors.email ? "is-invalid" : ""
                        }`}
                    />
                    <ErrorMessage
                      component="div"
                      name="email"
                      className="invalid-feedback"
                    />
                  </div>

                  <div className="form-group">
                    <label htmlFor="prize">Preferred Prize</label>

                    <Field
                      name="prize"
                      as="select"
                      className={`form-control ${touched.prize && errors.prize ? "is-invalid" : ""}`}>
                      <option value="any">Any</option>
                      <option value="jetbrains">Jetbrains</option>
                      <option value="ozcode">OzCode</option>
                      <option value="snagit">SnagIt</option>
                    </Field>
                    <ErrorMessage
                      component="div"
                      name="prize"
                      className="invalid-feedback"
                    />
                  </div>

                  <button
                    type="submit"
                    className="btn btn-primary btn-block"
                    disabled={isSubmitting}
                  >
                    {isSubmitting ? "Submitting" : "Submit"}
                  </button>
                </Form>
              )}
            </Formik>
          </div>
        </div>
      </div>
    );
  }
}