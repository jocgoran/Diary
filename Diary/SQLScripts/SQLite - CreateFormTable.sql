CREATE TABLE IF NOT EXISTS form
(
  id INT(11) NOT NULL,
  name varchar(60) NOT NULL,
  text varchar(60) NULL,
  PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS tab
(
  id INT(11) NOT NULL,
  name varchar(60) NOT NULL,
  PRIMARY KEY (id)
); 

CREATE TABLE IF NOT EXISTS asgmt_form_tab
(
  id INT(11) NOT NULL,
  form_id INT(11) NOT NULL,
  tab_id INT(11) NOT NULL,  
  sort INT(2) NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (form_id) REFERENCES form(id) ON DELETE CASCADE,
  FOREIGN KEY (tab_id) REFERENCES tab(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS field
(
  id INT(11) NOT NULL,
  name varchar(60) NOT NULL,
  type varchar(60) NOT NULL,
  text varchar(60) NULL,
  passwordChar varchar(60) NULL,
  maxLength INT(11) NULL, 
  PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS asgmt_form_field
(
  id INT(11) NOT NULL,
  form_id INT(11) NOT NULL,
  field_id INT(11) NOT NULL, 
  sort INT(2) NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (form_Id) REFERENCES form(id) ON DELETE CASCADE,
  FOREIGN KEY (field_id) REFERENCES field(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS asgmt_tab_field
(
  id INT(11) NOT NULL,
  tab_id INT(11) NOT NULL,
  field_id INT(11) NOT NULL, 
  sort INT(2) NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (tab_id) REFERENCES tab(id) ON DELETE CASCADE,
  FOREIGN KEY (field_id) REFERENCES field(id) ON DELETE CASCADE
);
