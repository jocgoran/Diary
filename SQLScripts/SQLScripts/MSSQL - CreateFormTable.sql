if not exists (select * from sysobjects where name='form' and xtype='U')
CREATE TABLE form
(
  id INT NOT NULL IDENTITY(1,1),
  name varchar(60) NOT NULL,
  Text varchar(60) NULL,
  PRIMARY KEY (id)
);

if not exists (select * from sysobjects where name='tab' and xtype='U')
CREATE TABLE tab
(
  id INT NOT NULL IDENTITY(1,1),
  name varchar(60) NOT NULL,
  PRIMARY KEY (id)
); 

if not exists (select * from sysobjects where name='asgmt_form_tab' and xtype='U')
CREATE TABLE asgmt_form_tab
(
  id INT NOT NULL IDENTITY(1,1),
  form_id INT NOT NULL,
  tab_id INT NOT NULL,  
  sort INT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (form_id) REFERENCES form(id) ON DELETE CASCADE,
  FOREIGN KEY (tab_id) REFERENCES tab(id) ON DELETE CASCADE
);

if not exists (select * from sysobjects where name='field' and xtype='U')
CREATE TABLE field
(
  id INT NOT NULL IDENTITY(1,1),
  name varchar(60) NOT NULL,
  type varchar(60) NOT NULL,
  Text varchar(60) NULL,
  PasswordChar varchar(60) NULL,
  MaxLength INT NULL, 
  ReadOnly BIT DEFAULT 0,
  Multiline BIT DEFAULT 0,
  PRIMARY KEY (id)
);

if not exists (select * from sysobjects where name='asgmt_form_field' and xtype='U')
CREATE TABLE asgmt_form_field
(
  id INT NOT NULL IDENTITY(1,1),
  form_id INT NOT NULL,
  field_id INT NOT NULL, 
  sort INT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (form_Id) REFERENCES form(id) ON DELETE CASCADE,
  FOREIGN KEY (field_id) REFERENCES field(id) ON DELETE CASCADE
);

if not exists (select * from sysobjects where name='asgmt_tab_field' and xtype='U')
CREATE TABLE asgmt_tab_field
(
  id INT NOT NULL IDENTITY(1,1),
  tab_id INT NOT NULL,
  field_id INT NOT NULL, 
  sort INT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (tab_id) REFERENCES tab(id) ON DELETE CASCADE,
  FOREIGN KEY (field_id) REFERENCES field(id) ON DELETE CASCADE
);
