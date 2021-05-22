import React, { useEffect, useReducer, useState } from 'react';

const isEmailValid = (address) => /^\S+@\S+\.\S+$/.test(address);

const emailReducer = (state, action) => {
  if (action.type === 'USER_INPUT') {
    return { value: action.value, isValid: isEmailValid(action.value), wasTouched: true };
  }

  if (action.type === 'INPUT_BLUR') {
    return { value: state.value, isValid: isEmailValid(state.value), wasTouched: true };
  }

  return { value: '', isValid: false, wasTouched: true }
}

const stringValueReducer = (state, action) => {
  if (action.type === 'USER_INPUT') {
    return { value: action.value, isValid: action.value.trim().length >= 1, wasTouched: true };
  }

  if (action.type === 'INPUT_BLUR') {
    return { value: state.value, isValid: state.value.trim().length >= 1, wasTouched: true };
  }

  return { value: '', isValid: false, wasTouched: true }
}

const DrawEntryForm = (props) => {
  const [formIsValid, setFormIsValid] = useState(false);
  const [emailState, dispatchEmail] = useReducer(emailReducer, { value: '', isValid: false, wasTouched: false });
  const [nameState, dispatchName] = useReducer(stringValueReducer, { value: '', isValid: false, wasTouched: false });
  const [prizeState, dispatchPrize] = useReducer(stringValueReducer, { value: '', isValid: false, wasTouched: false });

  const { isValid: prizeIsValid } = prizeState;
  const { isValid: emailIsValid } = emailState;
  const { isValid: nameIsValid } = nameState;

  const emailChangeHandler = (event) => {
    dispatchEmail({ type: 'USER_INPUT', value: event.target.value })
  }

  const nameChangeHandler = (event) => {
    dispatchName({ type: 'USER_INPUT', value: event.target.value })
  }

  const prizeChangeHandler = (event) => {
    dispatchPrize({ type: 'USER_INPUT', value: event.target.value })
  }

  const emailBlurHandler = (event) => {
    dispatchEmail({ type: 'INPUT_BLUR' })
  }

  const prizeBlurHandler = (event) => {
    dispatchPrize({ type: 'INPUT_BLUR' })
  }

  const nameBlurHandler = (event) => {
    dispatchName({ type: 'INPUT_BLUR' })
  }

  const submitHandlerAsync = async (event) => {
    event.preventDefault();
    props.errorHandler(null);
    props.ticketHandler(null);

    window.grecaptcha.ready(async () => {

      const token = await window.grecaptcha.execute('6LeegeEaAAAAANl1S0DTNQznlDEiqMYZAYLhQr5g', { action: 'submit' });

      const payload = {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          name: nameState.value,
          email: emailState.value,
          prize: prizeState.value,
          reCaptchaToken: token
        })
      }

      const response = await fetch('/api/drawEntry', payload);

      if (response.status == 201) {
        const jsonResponse = await response.json();
        props.ticketHandler(jsonResponse.ticketId);
      }
      else {
        props.errorHandler(true);
      }
    });
  }

  useEffect(() => {
    const debounceTimer = setTimeout(() => {
      setFormIsValid(
        emailIsValid && nameIsValid && prizeIsValid
      );
    }, 100);

    return () => {
      clearTimeout(debounceTimer);
    }
  }, [emailIsValid, nameIsValid, prizeIsValid])

  return (
    <div className="row">
      <div className="col-lg-12">
        <form onSubmit={submitHandlerAsync} noValidate>
          <div className='form-group'>
            <label htmlFor="name">Name</label>
            <input
              type="text"
              id="name"
              value={nameState.value}
              onChange={nameChangeHandler}
              onBlur={nameBlurHandler}
              className={`form-control ${nameState.wasTouched ? nameState.isValid === false ? 'is-invalid' : 'is-valid' : ''}`}
              placeholder="Al Paca"
            />
          </div>

          <div className='form-group'>
            <label htmlFor="email">Email</label>
            <input
              type="email"
              id="email"
              className={`form-control ${emailState.wasTouched ? emailState.isValid === false ? 'is-invalid' : 'is-valid' : ''}`}
              value={emailState.value}
              onChange={emailChangeHandler}
              onBlur={emailBlurHandler}
              placeholder="al.paca@dotnetnorth.org.uk"
            />
            <small id="emailHelp" className="form-text text-muted">We'll never share your email with anyone else.</small>
          </div>

          <div className="form-group">
            <label htmlFor="prize">Preferred Prize</label>

            <select
              className="custom-select"
              id="prize"
              className={`custom-select ${prizeState.wasTouched ? prizeState.isValid === false ? 'is-invalid' : 'is-valid' : ''}`}
              onChange={prizeChangeHandler}
              onBlur={prizeBlurHandler}
            >
              <option value="">Pick a prize...</option>
              <option value="any">Any</option>
              <option value="jetbrains">Jetbrains</option>
              <option value="ozcode">OzCode</option>
              <option value="snagit">SnagIt</option>
            </select>
          </div>

          <button type="submit" className="btn btn-primary btn-block" disabled={!formIsValid}>Submit</button>
        </form>
      </div>
    </div>
  );
}

export default DrawEntryForm;