import { message, Modal, Table } from "antd";
import { useForm } from "antd/lib/form/Form";
import moment from "moment";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { listAllPermissionTypes } from "../../redux/actions/permissionTypesAction";
import { permissionsAPI } from "../../services/api/permissionServiceAPI";
import { ColumnsPermission } from "./components/ColumnsPermission";
import { EditPermissionForm } from "./components/EditPermissionForm";

const initialRequest = {
  nombreEmpleado: "",
  pageNumber: 1,
  pageSize: 10,
};

export const PermissionsPage = () => {
  const [request, setRequest] = useState(initialRequest);
  const dispatch = useDispatch();
  const [form] = useForm();
  const [loadingButton, setLoadingButton] = useState(false);
  const [permissions, setPermissions] = useState({
    pageNumber: 1,
    pageSize: 10,
    total: 1,
    succeeded: false,
    message: "",
    errors: null,
    data: []
  });
  const [modalVisible, setModalVisible] = useState(false);
  const [loadingTable, setLoadingTable] = useState(false);

  useEffect(() => {
    dispatch(listAllPermissionTypes());
  }, [])  

  const openModalEdit = (item) => {    
    form.setFields([
      {
        name: "permisoId",
        value: item.permisoId
      },
      {
        name: "nombreEmpleado",
        value: item.nombreEmpleado,
      },
      {
        name: "apellidoEmpleado",
        value: item.apellidoEmpleado
      },
      {
        name: "tipoPermisoId",
        value: item.tipoPermisoId
      },     
      {
        name: "fechaPermiso",
        value: moment(item.fechaPermiso)
      }
    ]);
    setModalVisible(true);    
  }
  const handleModalCancel = () => {
    form.resetFields();
    setModalVisible(false);
  }
  const requestPermission = async (item) => {        
    try{
      setLoadingTable(true);
      const resp = await permissionsAPI.changedPermission({permisoId : item.permisoId});      
      if(resp.succeeded) {
        setRequest((prevState) => ({
          ...prevState,
          nombreEmpleado: "",
        }));        
        message.success(resp.message);
      }
      setTimeout(() => {
        setLoadingTable(false);  
      }, 1000);      
    }catch(ex){
      setLoadingTable(false);
      message.error(ex.message);
    }
  }
  const columns = ColumnsPermission(requestPermission, openModalEdit);

  useEffect(() => {
    (async () => {
      try {                
        const resp = await permissionsAPI.getAllPermissions(request);
        if(!resp.succeeded)
          message.error(resp.message);

        setPermissions(resp);              
      } catch (error) {
        message.error(error.message);        
      }
    })();
  }, [request]);

  const handleEditPermission = async (value) => {
    const body = {
      ...value,
      fechaPermiso: value.fechaPermiso.format("YYYY-MM-DDTHH:mm:ss.SSSS"),
    };
    try {      
      setLoadingButton(true);
      const resp = await permissionsAPI.updatePermission(body.permisoId, body);
      if (resp.succeeded) {
        handleModalCancel();
        setLoadingTable(true);
        setLoadingButton(false);
        message.success(resp.message);
        setRequest((prevState) => ({ ...prevState }));
        setTimeout(() => {
          setLoadingTable(false);
        }, 1000);        
      } else {
        setLoadingButton(false);        
        notification.error({
          message: "Error! :c",
          description: resp.message,
        });
      }
    } catch (error) {
      setLoadingButton(false);
      setLoadingTable(false);
      message.error(error.message);
    }    
  }
  const onPaginatedChange = (page) => {    
    setRequest((prevState) => ({
      ...prevState,
      pageNumber: page,
    }));
  };

  return (<>  
    <Table 
      columns={columns} 
      rowKey="permisoId" 
      loading={loadingTable}  
      scroll={{ x: 650 }} 
      dataSource={permissions.data}
      pagination={{
        total: 15,
        pageSize: permissions?.pageSize,
        current: permissions?.pageNumber,
        onChange: onPaginatedChange,
      }}
      />
      
    <Modal
        visible={modalVisible}
        title= "Editar permiso"        
        onCancel={handleModalCancel}
        footer={null}
      >
        <EditPermissionForm
          handleOnSubmit={handleEditPermission}
          loadingButton={loadingButton}
          form={form}
        />
      </Modal>
  </>)
};
