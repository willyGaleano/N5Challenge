import { Button, DatePicker, Form, Input, Select } from "antd"
import moment from "moment"
import { useSelector } from "react-redux";

export const EditPermissionForm = ({handleOnSubmit, loadingButton, form}) => {  
  const permissionTypes = useSelector((state) => state.permissionTypes);

  return (    
    <Form
      labelCol={{ span: 18 }}
      wrapperCol={{ span: 24 }}
      form={form}
      onFinish={handleOnSubmit}
      layout="vertical"
    >
      <Form.Item name="permisoId" hidden={true}>
            <Input />
      </Form.Item>
       <Form.Item
            name="nombreEmpleado"
            label="Nombres"
            rules={[
              { required: true, message: "Completa tu(s) nombre(s)" },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="apellidoEmpleado"
            label="Apellidos"
            rules={[
              { required: true, message: "Completa tu(s) apellido(s)" },
            ]}
          >
            <Input />
          </Form.Item>         
          <Form.Item
            name="tipoPermisoId"
            label="Tipo permiso"
            rules={[
              { required: true, message: "Completa el tipo de permiso" },
            ]}
          >
            <Select>
              {permissionTypes.map((item, index) => 
                  <Select.Option key={item.permissionTypeId} value={item.permissionTypeId}>{item.descripcion}</Select.Option>)
              }
        </Select>
          </Form.Item>
          <Form.Item
            name="fechaPermiso"
            label="Fecha permiso"            
            rules={[
              { required: true, message: "Completa la fecha de permiso" },
            ]}
          >
             <DatePicker
              style={{ width: "100%" }}
              format="YYYY-MM-DD HH:mm:ss"
              showTime={{ defaultValue: moment("00:00:00", "HH:mm:ss") }}
            />
          </Form.Item>
          <Form.Item>
        <Button
          htmlType="submit"
          type="primary"
          block={true}
          loading={loadingButton}
        >
          {loadingButton ? "Guardando..." : "Guardar"}
        </Button>
      </Form.Item>
    </Form>
  )
}
