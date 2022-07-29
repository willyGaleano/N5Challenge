import { message, Modal, Table } from "antd";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { permissionsAPI } from "../../services/api/permissionServiceAPI";
import { ColumnsPermission } from "./components/ColumnsPermission";
import { EditPermissionForm } from "./components/EditPermissionForm";

const initialRequest = {
  nombreEmpleado: "",
};

export const PermissionsPage = () => {
  const dispatch = useDispatch();
  const [request, setRequest] = useState(initialRequest);
  const [permissions, setPermissions] = useState([]);
  const [modalVisible, setModalVisible] = useState(false);
  const [loadingTable, setLoadingTable] = useState(false);
  
   useEffect(() => {
    (async () => {
      try {
        console.log("UseEffct");
        setLoadingTable(true);
        const resp = await permissionsAPI.getAllPermissions(request);
        console.log(resp.data.data);
        setPermissions(resp.data.data);        
        setLoadingTable(false);
        
      } catch (error) {
        console.log(error.message);
        setLoadingTable(false);
      }
    })();
  }, [request]);


  const openModalEdit = (item) => {
    setModalVisible(true);
    console.log(item);
  }

  const requestPermission = async (item) => {        
    try{
      const resp = await permissionsAPI.changedPermission({permisoId : item.permisoId});      
      if(resp.data.succeeded) {
        setRequest((prevState) => ({
          ...prevState,
          nombreEmpleado: "",
        }));        
        message.success(resp.data.message);
      }      
    }catch(ex){
      message.error(ex.message);      
    }
    
  }

  const columns = ColumnsPermission(requestPermission, openModalEdit);

  const handleModalCancel = () => {
     setRequest((prevState) => ({
      ...prevState,
      nombreEmpleado: "",
    }));
    setModalVisible(false);
  }
  const handleEditPermission = (value) => {
    console.log(value);
  }
  const onPaginatedChange = (page) => {
    setRequest((prevState) => ({
      ...prevState,
      nombreEmpleado: "",
    }));
  };

 

  return (<>  
    <Table 
      columns={columns} 
      rowKey="permisoId" 
      loading={loadingTable}  
      scroll={{ x: 650 }} 
      dataSource={permissions}       
      />
      
    <Modal
        visible={modalVisible}
        title= "Editar permiso"        
        onCancel={handleModalCancel}
        footer={null}
      >
        <EditPermissionForm/>
      </Modal>
  </>)
};
