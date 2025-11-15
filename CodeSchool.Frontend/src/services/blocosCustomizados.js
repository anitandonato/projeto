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

export function registrarBlocosCustomizados() {
  console.log('✅ Blocos customizados registrados!')
}