import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';

export class EmployeeEdit extends Component {
    static displayName = EmployeeEdit.name;

    constructor(props) {
        super(props);
        this.state = {
            fullName: '',
            birthdate: '',
            tin: '',
            typeId: 1,
            loading: false,
            loadingSave: false,
            formErrors: { fullName: '', birthdate: '', tin: '' },
            fullNameValid: false,
            birthdateValid: false,
            tinValid: false,
            formValid: false
        };
    }

    componentDidMount() {
        this.getEmployee(this.props.match.params.id);
    }

    errorClass(error) {
        return (error.length === 0 ? '' : 'is-invalid');
    }

    handleChange(event) {
        const name = event.target.name;
        const value = event.target.value;
        this.setState({ [name]: value }, () => { this.validateField(name, value) });

        //this.setState({ [event.target.name]: event.target.value });
    }

    handleSubmit(e) {
        e.preventDefault();

        this.validateField('fullName', '');
        this.validateField('birthdate', '');
        this.validateField('tin', '');

        if (!this.state.formValid) {
            return;
        }

        if (window.confirm("Are you sure you want to save?")) {
            this.saveEmployee();
        }

    }

    validateField(fieldName, value) {
        let fieldValidationErrors = this.state.formErrors;
        let fullNameValid = this.state.fullNameValid;
        let birthdateValid = this.state.birthdateValid;
        let tinValid = this.state.tinValid;

        switch (fieldName) {
            case 'fullName':
                fullNameValid = this.state.fullName ? true : false;
                fieldValidationErrors.fullName = fullNameValid ? '' : ' is required';
                break;

            case 'birthdate':
                birthdateValid = this.state.birthdate ? true : false;
                fieldValidationErrors.birthdate = birthdateValid ? '' : ' is required';
                break;

            case 'tin':
                tinValid = this.state.tin ? true : false;
                fieldValidationErrors.tin = tinValid ? '' : ' is required';
                break;

            default:
                break;
        }
        this.setState({
            formErrors: fieldValidationErrors,
            fullNameValid: fullNameValid,
            birthdateValid: birthdateValid,
            tinValid: tinValid,
        }, this.validateForm);
    }

    validateForm() {
        this.setState({ formValid: this.state.fullNameValid && this.state.birthdateValid && this.state.tinValid });
    }

    render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <div>
                <form className="contactForm">

                    <div className='form-row'>
                        <div className='form-group col-md-6'>
                            <label htmlFor='inputFullName4'>Full Name: *</label>
                            <input type='text' className={`form-control ${this.errorClass(this.state.formErrors.fullName)}`} id='inputFullName4' onChange={this.handleChange.bind(this)} name="fullName" value={this.state.fullName} placeholder='Full Name' required />
                        </div>
                        <div className='form-group col-md-6'>
                            <label htmlFor='inputBirthdate4'>Birthdate: *</label>
                            <input type='date' className={`form-control ${this.errorClass(this.state.formErrors.birthdate)}`} id='inputBirthdate4' onChange={this.handleChange.bind(this)} name="birthdate" value={this.state.birthdate} placeholder='Birthdate' required />
                        </div>
                    </div>
                    <div className="form-row">
                        <div className='form-group col-md-6'>
                            <label htmlFor='inputTin4'>TIN: *</label>
                            <input type='text' className={`form-control ${this.errorClass(this.state.formErrors.tin)}`} id='inputTin4' onChange={this.handleChange.bind(this)} value={this.state.tin} name="tin" placeholder='TIN' required />
                        </div>
                        <div className='form-group col-md-6'>
                            <label htmlFor='inputEmployeeType4'>Employee Type: *</label>
                            <select id='inputEmployeeType4' onChange={this.handleChange.bind(this)} value={this.state.typeId} name="typeId" className='form-control' required>
                                <option value='1'>Regular</option>
                                <option value='2'>Contractual</option>
                            </select>
                        </div>
                    </div>
                    <button type="submit" onClick={this.handleSubmit.bind(this)} disabled={this.state.loadingSave} className="btn btn-primary mr-2">{this.state.loadingSave ? "Loading..." : "Save"}</button>
                    <button type="button" onClick={() => this.props.history.push("/employees/index")} className="btn btn-primary">Back</button>
                </form>
            </div>;

        return (
            <div>
                <h1 id="tabelLabel" >Employee Create</h1>
                <p>All fields are required</p>
                {contents}
            </div>
        );
    }

    async saveEmployee() {
        this.setState({ loadingSave: true });
        const token = await authService.getAccessToken();
        const requestOptions = {
            method: 'PUT',
            headers: !token ? {} : { 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' },
            body: JSON.stringify(this.state)
        };
        const response = await fetch('api/employees/' + this.state.id, requestOptions);

        if (response.status === 200) {
            this.setState({ loadingSave: false });
            alert("Employee successfully saved");
            this.props.history.push("/employees/index");
        }
        else {
            alert("There was an error occured.");
        }
    }

    async getEmployee(id) {
        this.setState({ loading: true, loadingSave: false });
        const token = await authService.getAccessToken();
        const response = await fetch('api/employees/' + id, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();

        this.setState({ id: data.id, fullName: data.fullName, birthdate: data.birthdate, tin: data.tin, typeId: data.typeId, loading: false, loadingSave: false });
    }
}
