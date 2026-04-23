Feature: Pedido

Scenario: Criar pedido com combo completo
    When eu envio um pedido com sanduíche, batata e refrigerante
    Then o desconto deve ser 20%