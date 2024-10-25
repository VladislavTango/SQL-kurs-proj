export const RedactHttp = async (formData , SelectedKey) => {
  console.log(formData);
  
    switch(SelectedKey){
        case 1:{
        await fetch('https://localhost:7119/api/Doctor/redact', {
          method: 'PUT',
          headers: {
            'Accept': '*/*',
            'Content-Type': 'application/json'
          },
          body: JSON.stringify(formData)
        })
        break;
        }
        case 2:{
          await fetch('https://localhost:7119/api/Patient/redact', {
            method: 'PUT',
            headers: {
              'Accept': '*/*',
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
          })
        break;
        }
        default:console.log("):");
      };
      console.log(formData + "formdata");

      return formData;
  };
  