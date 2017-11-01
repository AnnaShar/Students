import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateControls';
import {isBirthDate} from '../moment';
import Tag from '../materialUIDecorators/tag';
import { NavLink } from 'react-router-dom';

export default class StudentTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  getRow(student, index)
  {
    return [
      index + 1,
      <div>
        {student.name}
        {student.tags.map((tag, index) => (<NavLink to={"/tag/" + tag} key={index}><Tag content={tag} /></NavLink>))}
      </div>,
      student.email,
      <span className={isBirthDate(student.birthDate) ? 'today' : ''}>{student.birthDate}</span>,
      student.groupName,
      student.startingDate,
      student.endingDate,
      <CandidateRowControls candidate={student} {...this.props}/>
    ];
  }

  render() {
    let rows = (this.props.students.map((student, index) =>
      this.getRow(student, index)
    ));

    return (
      <BasicTable
        heads={ ['#', 'Name', 'E-mail', 'Birth Date',  'Group', 'Starting Date', 'Ending date',
          <span className="float-right">Actions</span>] }
        contentRows={rows}
      />
    );
  }
}

StudentTable.propTypes = {
  students: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};