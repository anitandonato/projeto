import * as Blockly from 'blockly'
import { javascriptGenerator } from 'blockly/javascript'

// Bloco: Mover para frente
Blockly.Blocks['mover_frente'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("🤖 Mover para frente");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(160);
    this.setTooltip("Move o robô uma casa para frente");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['mover_frente'] = function(block) {
  return 'mover();\n';
};

// Bloco: Virar à direita
Blockly.Blocks['virar_direita'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("↪️ Virar à direita");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
    this.setTooltip("Vira o robô 90° para a direita");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['virar_direita'] = function(block) {
  return 'virarDireita();\n';
};

// Bloco: Virar à esquerda
Blockly.Blocks['virar_esquerda'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("↩️ Virar à esquerda");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
    this.setTooltip("Vira o robô 90° para a esquerda");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['virar_esquerda'] = function(block) {
  return 'virarEsquerda();\n';
};

// ========== BLOCOS DE CONDICIONAIS ==========

// Bloco: Se (if)
Blockly.Blocks['se'] = {
  init: function() {
    this.appendValueInput("CONDICAO")
        .setCheck("Boolean")
        .appendField("🔍 Se");
    this.appendStatementInput("FAZER")
        .setCheck(null)
        .appendField("fazer");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(210);
    this.setTooltip("Executa comandos se a condição for verdadeira");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['se'] = function(block) {
  const condicao = javascriptGenerator.valueToCode(block, 'CONDICAO', javascriptGenerator.ORDER_NONE) || 'false';
  const fazer = javascriptGenerator.statementToCode(block, 'FAZER');
  return `if (${condicao}) {\n${fazer}}\n`;
};

// Bloco: Se/Senão (if/else)
Blockly.Blocks['se_senao'] = {
  init: function() {
    this.appendValueInput("CONDICAO")
        .setCheck("Boolean")
        .appendField("🔍 Se");
    this.appendStatementInput("FAZER")
        .setCheck(null)
        .appendField("fazer");
    this.appendStatementInput("SENAO")
        .setCheck(null)
        .appendField("senão");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(210);
    this.setTooltip("Executa comandos diferentes dependendo da condição");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['se_senao'] = function(block) {
  const condicao = javascriptGenerator.valueToCode(block, 'CONDICAO', javascriptGenerator.ORDER_NONE) || 'false';
  const fazer = javascriptGenerator.statementToCode(block, 'FAZER');
  const senao = javascriptGenerator.statementToCode(block, 'SENAO');
  return `if (${condicao}) {\n${fazer}} else {\n${senao}}\n`;
};

// Bloco: Tem parede na frente?
Blockly.Blocks['tem_parede_frente'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("🧱 Tem parede na frente?");
    this.setOutput(true, "Boolean");
    this.setColour(290);
    this.setTooltip("Verifica se há uma parede bloqueando o caminho");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['tem_parede_frente'] = function(block) {
  return ['temParedeNaFrente()', javascriptGenerator.ORDER_NONE];
};

// Bloco: Está no objetivo?
Blockly.Blocks['esta_no_objetivo'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("🚩 Está no objetivo?");
    this.setOutput(true, "Boolean");
    this.setColour(290);
    this.setTooltip("Verifica se o robô chegou na bandeira");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['esta_no_objetivo'] = function(block) {
  return ['estaNoObjetivo()', javascriptGenerator.ORDER_NONE];
};

// ========== BLOCOS DE VARIÁVEIS ==========

// Bloco: Criar/Definir variável
Blockly.Blocks['definir_variavel'] = {
  init: function() {
    this.appendValueInput("VALOR")
        .setCheck("Number")
        .appendField("📦 Definir")
        .appendField(new Blockly.FieldVariable("contador"), "VAR")
        .appendField("=");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(330);
    this.setTooltip("Define o valor de uma variável");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['definir_variavel'] = function(block) {
  const variable = javascriptGenerator.nameDB_.getName(block.getFieldValue('VAR'), Blockly.Variables.NAME_TYPE);
  const valor = javascriptGenerator.valueToCode(block, 'VALOR', javascriptGenerator.ORDER_ASSIGNMENT) || '0';
  return `${variable} = ${valor};\n`;
};

// Bloco: Obter valor da variável
Blockly.Blocks['obter_variavel'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("📦")
        .appendField(new Blockly.FieldVariable("contador"), "VAR");
    this.setOutput(true, "Number");
    this.setColour(330);
    this.setTooltip("Obtém o valor da variável");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['obter_variavel'] = function(block) {
  const variable = javascriptGenerator.nameDB_.getName(block.getFieldValue('VAR'), Blockly.Variables.NAME_TYPE);
  return [variable, javascriptGenerator.ORDER_ATOMIC];
};

// Bloco: Incrementar variável
Blockly.Blocks['incrementar_variavel'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("➕ Incrementar")
        .appendField(new Blockly.FieldVariable("contador"), "VAR");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(330);
    this.setTooltip("Adiciona 1 à variável");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['incrementar_variavel'] = function(block) {
  const variable = javascriptGenerator.nameDB_.getName(block.getFieldValue('VAR'), Blockly.Variables.NAME_TYPE);
  return `${variable}++;\n`;
};

// Bloco: Comparação (igual, maior, menor)
Blockly.Blocks['comparacao'] = {
  init: function() {
    this.appendValueInput("A")
        .setCheck("Number");
    this.appendValueInput("B")
        .setCheck("Number")
        .appendField(new Blockly.FieldDropdown([
          ["=", "=="],
          [">", ">"],
          ["<", "<"],
          ["≥", ">="],
          ["≤", "<="],
          ["≠", "!="]
        ]), "OP");
    this.setInputsInline(true);
    this.setOutput(true, "Boolean");
    this.setColour(210);
    this.setTooltip("Compara dois valores");
    this.setHelpUrl("");
  }
};

javascriptGenerator.forBlock['comparacao'] = function(block) {
  const a = javascriptGenerator.valueToCode(block, 'A', javascriptGenerator.ORDER_RELATIONAL) || '0';
  const op = block.getFieldValue('OP');
  const b = javascriptGenerator.valueToCode(block, 'B', javascriptGenerator.ORDER_RELATIONAL) || '0';
  return [`${a} ${op} ${b}`, javascriptGenerator.ORDER_RELATIONAL];
};

export function registrarBlocosCustomizados() {
  console.log('✅ Blocos customizados registrados!')
}