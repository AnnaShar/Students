import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import IconButton from '../materialUIDecorators/iconButton';
import OpenIcon from 'material-ui-icons/OpenInNew';
import UploadIcon from 'material-ui-icons/FileUpload';
import DownloadIcon from 'material-ui-icons/FileDownload';

const iconButtonStyle = {
  height: 32,
  width: 32,
};

export default class ResumeControls extends React.Component {
  render() {
    let fileIsLoaded = true;
    if (!this.props.fileName || this.props.fileName.trim() === '') {
      fileIsLoaded = false;
    }
    return (
        fileIsLoaded ?
          <Wrapper>
            <FileName>{this.props.fileName}</FileName>
            <IconButton style={iconButtonStyle} onClick={() => {}} icon={<OpenIcon/>}/>
            <IconButton style={iconButtonStyle} onClick={()=>{}} icon={<DownloadIcon/>}/>
            <IconButton style={iconButtonStyle} onClick={()=>{}} icon={<UploadIcon/>}/>
          </Wrapper>
          :
          <Wrapper>
            <NotLoaded>not loaded</NotLoaded>
            <IconButton style={iconButtonStyle} onClick={()=>{}} icon={<UploadIcon/>}/>
          </Wrapper>
    );
  }
}

ResumeControls.propTypes = {
  fileName: PropTypes.string,
};

const Wrapper = styled.div`
  display: flex;
  align-items: center;
`;

const FileName = styled.span`
  font-style: italic;
`;

const NotLoaded = styled.span`
  font-style: italic;
  color: rgba(0, 0, 0, 0.6);
`;